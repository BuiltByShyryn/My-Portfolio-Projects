from flask import Flask, render_template, redirect, url_for,request 
import sqlite3

app = Flask(__name__)


def get_db_connection():
    conn= sqlite3.connect ('site.db')
    conn.row_factory = sqlite3.Row
    return conn

def create_table():
    conn = sqlite3.connect('site.db')
    c = conn.cursor()
    c.execute(''' 
    CREATE TABLE IF NOT EXISTS brands(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            description TEXT NOT NULL
    );            
''')
    conn.commit()
    conn.close()
create_table()


def create_categories_table():  
    conn= sqlite3.connect('site.db')
    c = conn.cursor()
    c.execute('''
        CREATE TABLE IF NOT EXISTS categories(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            description TEXT NOT NULL,
            brand_id INTEGER,
            FOREIGN KEY (brand_id) REFERENCES brands(id)  
        );
''')
    conn.commit()
    conn.close()
    
create_categories_table()

def create_products_table():  
    conn = sqlite3.connect('site.db')
    c = conn.cursor()
    c.execute('''
        CREATE TABLE IF NOT EXISTS products(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            description TEXT NOT NULL,
            price REAL NOT NULL,
            brand_id INTEGER,
            category_id INTEGER,
            FOREIGN KEY (brand_id) REFERENCES brands(id),
            FOREIGN KEY (category_id) REFERENCES categories(id)
        );
    ''')
    conn.commit()
    conn.close()
create_products_table()


@app.route('/')
def index():
    conn = get_db_connection()
    brands = conn.execute('SELECT * FROM brands').fetchall()
    categories = conn.execute('SELECT * FROM categories').fetchall()
    conn.close()
    return render_template('index.html', brands=brands,categories = categories)

@app.route('/add',methods = ['GET', 'POST'])
def add_brand():
    if request.method == 'POST':
        name = request.form['name']
        description = request.form['description']
        conn = get_db_connection()
        conn.execute("INSERT INTO brands (name, description) VALUES (?,?)", (name,description))
        conn.commit()
        return redirect(url_for('index'))
    return render_template('add_brand.html')

@app.route('/edit/<int:id>', methods = ["GET", "POST"])
def edit_brand(id):
    conn = get_db_connection()
    brand = conn.execute('SELECT * FROM brands WHERE id=?',(id,)).fetchone()
    if request.method == 'POST':
        name = request.form ['name']
        description = request.form['description']
        conn.execute('UPDATE brands SET name = ?, description = ? WHERE id = ?', (name,description, id))
        conn.commit()
        conn.close()
        return redirect (url_for('index'))
    conn.close()
    return render_template('edit_brand.html', brand = brand)

@app.route('/delete/<int:id>')
def delete_brand_and_category(id):
    conn=get_db_connection()
    conn.execute('DELETE FROM brands WHERE id = ?', (id,))
    conn.execute('DELETE FROM categories WHERE brand_id = ?', (id,))
    conn.commit()
    conn.close()
    return redirect(url_for('index'))

@app.route('/add_category', methods = ['POST', 'GET'])
def add_category():
    if request.method == 'POST':
        name = request.form['name']
        description = request.form['description']
        brand_id = request.form.get('brand_id')
        conn = get_db_connection()
        conn.execute("INSERT INTO categories (name, description, brand_id) values (?,?,?)", (name,description,brand_id) )
        conn.commit()
        conn.close()
        return redirect(url_for('index'))
    return render_template('add_category.html')

@app.route('/edit_category/<int:id>', methods = ['POST', 'GET'])
def edit_category(id):
    conn = get_db_connection()
    category = conn.execute (' SELECT * FROM  categories WHERE id = ?', (id,)).fetchone()
    if request.method == 'POST':
        name = request.form['name']
        description = request.form['description']
        brand_id = request.form['brand_id']
        conn.execute("UPDATE categories SET name = ?, description = ?, brand_id = ? WHERE id = ?", (name,description,brand_id,id))
        conn.commit ()
        conn.close()
        return redirect(url_for('index'))
    conn.close()
    return render_template('edit_category.html', category = category)

@app.route('/delete_category/<int:id>')
def delete_category(id):
    conn=get_db_connection()
    conn.execute("DELETE FROM categories WHERE id = ?", (id,))
    conn.commit()
    conn.close()
    return redirect(url_for('index'))

@app.route('/add_product', methods=['GET', 'POST'])
def add_product():
    conn = get_db_connection()
    brands = conn.execute('SELECT * FROM brands').fetchall()
    categories = conn.execute('SELECT * FROM categories').fetchall()
    conn.close()

    if request.method == 'POST':
        name = request.form['name']
        description = request.form['description']
        price = request.form['price']
        brand_id = request.form['brand_id']
        category_id = request.form['category_id']
        conn = get_db_connection()
        conn.execute("INSERT INTO products (name, description, price, brand_id, category_id) VALUES (?,?,?,?,?)",
                     (name, description, price, brand_id, category_id))
        conn.commit()
        conn.close()
        return redirect(url_for('index'))
    return render_template('add_product.html', brands=brands, categories=categories)

@app.route('/edit_product/<int:id>', methods=['GET', 'POST'])
def edit_product(id):
    conn = get_db_connection()
    product = conn.execute('SELECT * FROM products WHERE id=?', (id,)).fetchone()
    brands = conn.execute('SELECT * FROM brands').fetchall()
    categories = conn.execute('SELECT * FROM categories').fetchall()
    conn.close()

    if request.method == 'POST':
        name = request.form['name']
        description = request.form['description']
        price = request.form['price']
        brand_id = request.form['brand_id']
        category_id = request.form['category_id']
        conn = get_db_connection()
        conn.execute("UPDATE products SET name=?, description=?, price=?, brand_id=?, category_id=? WHERE id=?",
                     (name, description, price, brand_id, category_id, id))
        conn.commit()
        conn.close()
        return redirect(url_for('index'))
    return render_template('edit_product.html', product=product, brands=brands, categories=categories)

@app.route('/delete_product/<int:id>')
def delete_product(id):
    conn = get_db_connection()
    conn.execute('DELETE FROM products WHERE id=?', (id,))
    conn.commit()
    conn.close()
    return redirect(url_for('index'))


if __name__=='__main__':
    app.run()