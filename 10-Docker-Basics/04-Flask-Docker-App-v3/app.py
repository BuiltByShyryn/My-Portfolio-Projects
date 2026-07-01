from flask import Flask, render_template

app = Flask(__name__)

@app.route("/")
def home():
    return render_template("index.html")

@app.route("/post")
def post():
    return render_template("post.html")

@app.route("/docker")
def docker():
    return render_template("docker.html")

app.run(host="0.0.0.0", port=5000)