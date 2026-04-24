from flask import Flask, render_template

app = Flask(__name__)
@app.route('/')
def home():
    return "Welcome! Go to /products or /team"


@app.route('/products')
def products():
    products = [
        {"name": "Laptop", "price": 1000},
        {"name": "Phone", "price": 500},
        {"name": "Headphones", "price": 100}
    ]
    return render_template('products.html', products=products)

@app.route('/team')
def team():
    team = [
        {"name": "Alice", "position": "Developer"},
        {"name": "Bob", "position": "Designer"},
        {"name": "Charlie", "position": "Manager"}
    ]
    return render_template('team.html', team=team)

if __name__ == "__main__":
    app.run(debug=True)
