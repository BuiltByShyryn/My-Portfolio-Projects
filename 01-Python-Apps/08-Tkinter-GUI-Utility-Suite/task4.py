import tkinter as tk 
import random
from tkinter import messagebox

root = tk.Tk()
root.title("Random fact")

quotes = [
    "Octopuses have three hearts.",
    "Bananas are berries, but strawberries aren't.",
    "A day on Venus is longer than a year on Venus.",
    "Butterflies can taste with their feet.",
    "Sharks existed before trees.",
    "Wombat poop is cube-shaped.",
    "The Eiffel Tower grows taller in summer due to heat expansion.",
    "Honey never spoils and can last for thousands of years.",
    "Sloths can hold their breath longer than dolphins.",
    "There are more stars in the universe than grains of sand on Earth's beaches.",
    "The heart of a blue whale is so large a human could swim through its arteries.",
    "Koalas have unique fingerprints, just like humans.",
    "Some jellyfish are biologically immortal.",
    "Cows have best friends and get stressed when separated.",
    "Your brain generates enough electricity to power a lightbulb."
]
def fact():
    n = random.choice(quotes)
    messagebox.showinfo("Result:", f"{n}")
    

label = tk.Label(root, text="Rnadom fact programm")
label.pack(pady = 10)

button = tk.Button(root, text="Press me", command = fact)
button.pack(pady = 5)