from flask import Flask, request, redirect, render_template, url_for
import sqlite3
def connection ():
    conn = sqlite3.connect("New_database.db")
    conn.row_factory = sqlite3.Row
    return conn 

app = Flask(__name__)


@app.route("/")
def home():
    return render_template("index.html")


@app.route("/categories", methods = ["GET", "POST"])
def categories():
    conn = connection()
    cursor = conn.cursor()
    if request.method == 'POST':
        category_name = request.form.get('name')
        cursor.execute("INSERT INTO categories (name) VALUES (?)", (category_name,))
        conn.commit()
        conn.close()
        
        return redirect (url_for('categories'))
    
    cursor.execute("SELECT * FROM categories")
    categories = cursor.fetchall()
    conn.close()

    return render_template('categories.html', categories = categories)
 
@app.route("/delete_category/<int:id_category>")
def delete_category(id_category):
    conn = connection()
    cursor = conn.cursor()
    cursor.execute("DELETE FROM categories WHERE id_category = ? ", (id_category,))
    conn.commit()
    conn.close()
    return redirect(url_for('categories'))

@app.route("/articles", methods = ["GET"])
def articles():
    conn = connection()
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM articles")
    articles = cursor.fetchall()
    conn.close()
    
    return render_template("articles.html", articles = articles)


@app.route("/articles/<int:id>", methods = ["GET"])
def article_detail(id):
    conn = connection()
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM articles WHERE id_article = ?",(id,))
    article = cursor.fetchone()
    conn.close()
    
    return render_template("article.html", article = article)

@app.route("/edit_article/<int:id>", methods = ["POST", "GET"])
def edit_article(id):
    conn = connection()
    cursor = conn.cursor()
    
    if request.method =="POST":
        new_title=request.form["title"]
        
        cursor.execute("UPDATE articles SET title = ? WHERE id_article = ?", (new_title,id))
        conn.commit()
        conn.close()
        return redirect(f"/article/{id}")
    
    cursor.execute("SELECT * FROM articles WHERE id_article = ?", (id,))
    article = cursor.fetchone()
    conn.close()
    return render_template("Edit_article.html", article = article)

@app.route("/delete_article/<int:id>", methods = ["POST"])
def delete_article(id):
    conn = connection()
    cursor = conn.cursor()
    
    if request.method == "POST":
        cursor.execute("DELETE FROM articles WHERE id_article=?",(id,))
        conn.commit()
        conn.close()
        return redirect(url_for("home"))
    
    

if __name__ == '__main__':
    app.run()
