from flask import Flask, render_template, request, redirect, url_for,session
import sqlite3

app = Flask(__name__)
app.secret_key = "secretkey123"



def connect_db():
    return sqlite3.connect("database.db")

def log_page_view():
    user_id = session.get('user_id')
    with sqlite3.connect('database.db') as conn:  
        cursor = conn.cursor()
        cursor.execute("INSERT INTO page_views (timestamp) VALUES (datetime('now'))")
        conn.commit()


@app.route("/")
def home():
    log_page_view()
    conn = connect_db()
    cursor = conn.cursor()
    
    cursor.execute("SELECT * FROM products") 
    products = cursor.fetchall()
    cursor.execute("SELECT * FROM categories")
    categories = cursor.fetchall()
    conn.close()
    return render_template("index.html", products=products,categories= categories)


@app.route("/categories")
def categories():
    try:
        conn = connect_db()
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM categories")  
        categories = cursor.fetchall()
        conn.close()
        return render_template("categories.html", categories=categories)
    except Exception as e:
        return f"An error occurred: {e}"


@app.route("/category/<category_id>")
def category(category_id):
    try:
        conn = connect_db()
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM products WHERE category_id = ?", (category_id,))
        products = cursor.fetchall()
        conn.close()
        
        print(f"Category ID: {category_id}")
        print(F"Products found: {products}")
        
        return render_template("category_page.html", products=products)
    except Exception as e:
        return f"An error occurred: {e}"  



@app.route("/register", methods=["GET", "POST"])
def register():
    if request.method == "POST":
        username = request.form["username"]
        email = request.form["email"]
        password = request.form["password"]  

        conn = connect_db()
        cursor = conn.cursor()
        cursor.execute("INSERT INTO users (username, email, password) VALUES (?, ?, ?)", (username, email, password))
        conn.commit()
        conn.close()

        return redirect(url_for("login"))

    return render_template("register.html")

@app.route("/login", methods=["GET", "POST"])
def login():
    if request.method == "POST":
        username = request.form["username"]
        password = request.form["password"]

        conn = connect_db()
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM users WHERE username = ? AND password = ?", (username, password))
        user = cursor.fetchone()
        conn.close()

        if user:
            session['user_id'] = user[0] 
            session['admin'] = int(user[4])
            return redirect(url_for("home"))
        else:
            return "Invalid username or password."

    return render_template("login.html")


@app.route("/logout")
def logout():
    session.pop('user_id', None)
    return redirect(url_for("home"))


@app.route("/add_to_cart/<int:product_id>")
def add_to_cart(product_id):
    user_id = session.get('user_id')
    conn = connect_db()
    cursor = conn.cursor()
    cursor.execute("INSERT INTO cart (user_id, product_id, quantity) VALUES (?, ?, 1)", (user_id, product_id))
    conn.commit()
    conn.close()
    return redirect(url_for("cart")) 

@app.route("/cart")
def cart():
    user_id = session.get('user_id')  
    conn = connect_db()
    cursor = conn.cursor()
    cursor.execute("""
        SELECT products.name, products.price, cart.quantity FROM cart
        JOIN products ON cart.product_id = products.id
        WHERE cart.user_id = ?
    """, (user_id,))
    cart_items = cursor.fetchall()
    conn.close()
    return render_template("cart.html", cart_items=cart_items)

@app.route("/search", methods=["GET"])
def search():
    query = request.args.get("q", "").strip()
    
    if not query:  
        return render_template("search.html", results=[])

    conn = connect_db()
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM products WHERE name LIKE ?", ('%' + query + '%',))
    results = cursor.fetchall()
    conn.close()
    
    return render_template("search.html", results=results)

 
@app.route("/checkout")
def checkout():
    user_id = session.get('user_id')
    conn = connect_db()
    cursor = conn.cursor()
    cursor.execute("""
        SELECT products.name, products.price, cart.quantity FROM cart
        JOIN products ON cart.product_id = products.id
        WHERE cart.user_id = ?
    """, (user_id,))
    cart_items = cursor.fetchall()
    conn.close()
    return render_template("checkout.html", cart_items=cart_items)




@app.route('/order_confirmation', methods=['GET', 'POST'])
def order_confirmation():
    user_id = session.get('user_id')
    if not user_id:
        return redirect(url_for('login'))

    conn = connect_db()
    cursor = conn.cursor()
    
    
    cursor.execute("""
        SELECT products.name, products.price, cart.quantity FROM cart
        JOIN products ON cart.product_id = products.id
        WHERE cart.user_id = ?
    """, (user_id,))
    
    cart_items = cursor.fetchall()
    total_price = sum(item[1] * item[2] for item in cart_items)
    
   
    cursor.execute("DELETE FROM cart WHERE user_id = ?", (user_id,))
    conn.commit()
    
    conn.close()
    return render_template('order_confirmation.html', cart_items=cart_items, total_price=total_price)



@app.route("/admin", methods=["GET", "POST"])
def admin():
    if session.get("admin") != 1:  
        return "Access denied"
    
    conn = connect_db()
    cursor = conn.cursor()
    cursor.execute("SELECT COUNT(*) FROM page_views")
    total_views = cursor.fetchone()[0]
    cursor.execute("SELECT * FROM products")
    products = cursor.fetchall()
    cursor.execute("SELECT id,name FROM categories")
    categories = cursor.fetchall()
    conn.close()
    
    return render_template("admin.html", total_views=total_views, products=products,categories=categories)

@app.route("/add_product", methods=["POST"])
def add_product_to_store():
    if session.get("admin") != 1:
        return "Access denied"

    name, price, category_id = request.form.get("name"), request.form.get("price"), request.form.get("category_id")
    if not all([name, price, category_id]):
        return "All fields are required", 400

    try:
        conn = connect_db()
        conn.execute("INSERT INTO products (name, price, category_id) VALUES (?, ?, ?)", (name, price, category_id))
        conn.commit()
    except sqlite3.IntegrityError as e:
        return f"Database error: {e}", 500
    finally:
        conn.close()

    return redirect(url_for("admin"))

@app.route("/add_to_cart/<int:product_id>", methods = ["POST"])
def add_to_cart_user(product_id):  
    user_id = session.get('user_id') 

    if not user_id:
        return redirect(url_for('login'))  

    conn = connect_db()
    cursor = conn.cursor()


    cursor.execute("SELECT * FROM cart WHERE user_id = ? AND product_id = ?", (user_id, product_id))
    existing_item = cursor.fetchone()
    
    if existing_item:
        
        cursor.execute("UPDATE cart SET quantity = quantity + 1 WHERE user_id = ? AND product_id = ?", (user_id, product_id))
    else:

        cursor.execute("INSERT INTO cart (user_id, product_id, quantity) VALUES (?, ?, 1)", (user_id, product_id))

    conn.commit()
    conn.close()
    
    return redirect(url_for("cart"))

@app.route("/profile")
def profile():
    if "user_id" not in session:
        return redirect(url_for("login")) 

    user_id = session.get('user_id')

    
    
    conn = connect_db()
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM users WHERE id = ?", (session["user_id"],))
    user = cursor.fetchone()
    cursor.execute("SELECT COUNT(*) FROM page_views WHERE id = ?", (user_id,))
    total_views = cursor.fetchone()[0]
    conn.close()

    return render_template("profile.html", user=user,total_views=total_views)

@app.route("/edit_profile", methods=["GET", "POST"])
def edit_profile():
    if "user_id" not in session:
        return redirect(url_for("login"))  
    
    conn = connect_db()
    cursor = conn.cursor()
    
    user_id = session["user_id"]
    
    if request.method == "POST":
        new_name = request.form["name"]
        new_email = request.form["email"]
        
        cursor.execute("UPDATE users SET name = ?, email = ? WHERE id = ?", (new_name, new_email, user_id))
        conn.commit()
    
    cursor.execute("SELECT name, email FROM users WHERE id = ?", (user_id,))
    user = cursor.fetchone()
    
    conn.close()
    
    return render_template("edit_profile.html", user=user)

 
if __name__ == "__main__":
    app.run()
