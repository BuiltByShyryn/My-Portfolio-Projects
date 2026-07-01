from flask import Flask, render_template, request, redirect
import os

app = Flask(__name__)

FILE_NAME = "data/products.txt"

if not os.path.exists(FILE_NAME):
    with open(FILE_NAME, "w", encoding="utf-8"):
        pass


@app.route("/", methods=["GET", "POST"])
def index():

    if request.method == "POST":

        name = request.form["name"]
        quantity = request.form["quantity"]
        price = request.form["price"]

        with open(FILE_NAME, "a", encoding="utf-8") as file:
            file.write(f"{name}|{quantity}|{price}\n")

        return redirect("/")

    products = []

    with open(FILE_NAME, "r", encoding="utf-8") as file:
        for line in file:
            products.append(line.strip().split("|"))

    return render_template("index.html", products=products)


app.run(host="0.0.0.0", port=5000)