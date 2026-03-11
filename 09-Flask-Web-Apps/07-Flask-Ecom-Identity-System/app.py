import sqlite3
from flask import Flask, render_template, request, redirect, url_for

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

def add_product(name, description, price, stock):
    conn = sqlite3.connect(DATABASE)
    cursor = conn.cursor()
    cursor.execute('INSERT INTO products (name, description, price, stock) VALUES (?, ?, ?, ?)', 
                   (name, description, price, stock))
    conn.commit()
    conn.close()

def register_user(name, surname, email, phone, password):
    conn = sqlite3.connect(DATABASE)
    cursor = conn.cursor()
    cursor.execute('''
    CREATE TABLE IF NOT EXISTS users (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        name TEXT,
        surname TEXT,
        email TEXT,
        phone TEXT,
        password TEXT
    )''')
    cursor.execute('INSERT INTO users (name, surname, email, phone, password) VALUES (?, ?, ?, ?, ?)', 
                   (name, surname, email, phone, password))
    conn.commit()
    conn.close()

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

@app.route('/products/add', methods=['GET', 'POST'])
def add_product_page():
    if request.method == 'POST':
        name = request.form['name']
        description = request.form['description']
        price = request.form['price']
        stock = request.form['stock']
        add_product(name, description, price, stock)
        return redirect(url_for('index'))
    return render_template('add_product.html')

@app.route('/register', methods=['GET', 'POST'])
def register():
    if request.method == 'POST':
        name = request.form['name']
        surname = request.form['surname']
        email = request.form['email']
        phone = request.form['phone']
        password = request.form['password']
        confirm_password = request.form['confirm_password']
        
        if password != confirm_password:
            return "Passwords do not match"
        
        register_user(name, surname, email, phone, password)
        return render_template('registration_success.html', name=name)
    return render_template('register.html')

if __name__ == '__main__':
    init_db()
    app.run(debug=True)
