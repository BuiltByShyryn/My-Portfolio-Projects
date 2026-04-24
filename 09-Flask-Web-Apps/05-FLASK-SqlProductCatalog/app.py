import sqlite3
from flask import Flask, render_template

app = Flask(__name__)

DATABASE = 'products.db'

def init_db():
    conn = sqlite3.connect(DATABASE)
    cursor = conn.cursor()
    cursor.execute('''
    CREATE TABLE IF NOT EXISTS products (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        name TEXT NOT NULL,
        description TEXT,
        price REAL,
        stock INTEGER
    )''')
    cursor.execute('INSERT INTO products (name, description, price, stock) VALUES (?, ?, ?, ?)', 
                   ('Product 1', 'Description of product 1', 10.99, 100))
    cursor.execute('INSERT INTO products (name, description, price, stock) VALUES (?, ?, ?, ?)', 
                   ('Product 2', 'Description of product 2', 20.99, 200))
    cursor.execute('INSERT INTO products (name, description, price, stock) VALUES (?, ?, ?, ?)', 
                   ('Product 3', 'Description of product 3', 30.99, 300))
    cursor.execute('INSERT INTO products (name, description, price, stock) VALUES (?, ?, ?, ?)', 
                   ('Product 4', 'Description of product 4', 40.99, 400))
    cursor.execute('INSERT INTO products (name, description, price, stock) VALUES (?, ?, ?, ?)', 
                   ('Product 5', 'Description of product 5', 50.99, 500))
    conn.commit()
    conn.close()

def get_products():
    conn = sqlite3.connect(DATABASE)
    cursor = conn.cursor()
    cursor.execute('SELECT * FROM products')
    products = cursor.fetchall()
    conn.close()
    return products

def get_product_by_id(product_id):
    conn = sqlite3.connect(DATABASE)
    cursor = conn.cursor()
    cursor.execute('SELECT * FROM products WHERE id = ?', (product_id,))
    product = cursor.fetchone()
    conn.close()
    return product

@app.route('/')
def index():
    products = get_products()
    return render_template('index.html', products=products)

@app.route('/products/<int:id>')
def product_details(id):
    product = get_product_by_id(id)
    if product:
        return render_template('product_details.html', product=product)
    else:
        return "Product not found", 404

if __name__ == '__main__':
    init_db()
    app.run(debug=True)
