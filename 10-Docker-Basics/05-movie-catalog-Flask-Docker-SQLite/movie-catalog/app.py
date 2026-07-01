from flask import Flask, render_template, request, redirect
import sqlite3
import os

app = Flask(__name__)

DATABASE = "data/app.db"


def init_db():
    connection = sqlite3.connect(DATABASE)
    cursor = connection.cursor()

    cursor.execute("""
        CREATE TABLE IF NOT EXISTS movies (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT,
            genre TEXT,
            year INTEGER,
            rating REAL
        )
    """)

    connection.commit()
    connection.close()


@app.route("/")
def index():
    return render_template("index.html")

@app.route("/add", methods=["GET", "POST"])
def add():

    if request.method == "POST":

        title = request.form["title"]
        genre = request.form["genre"]
        year = request.form["year"]
        rating = request.form["rating"]

        connection = sqlite3.connect(DATABASE)
        cursor = connection.cursor()

        cursor.execute("""
            INSERT INTO movies (title, genre, year, rating)
            VALUES (?, ?, ?, ?)
        """, (title, genre, year, rating))

        connection.commit()
        connection.close()

        return redirect("/movies")

    return render_template("add.html")


init_db()

app.run(host="0.0.0.0", port=5000)