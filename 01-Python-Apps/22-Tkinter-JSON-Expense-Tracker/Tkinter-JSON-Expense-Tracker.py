import tkinter as tk
import json 
from tkinter import simpledialog

root = tk.Tk()
root.title("Tracker")
data = []
with open("expenses.json", "w")as file:
    json.dump(data, file)

label = tk.Label(root, text = "Expenses:")
label.pack()

entry = tk.Entry(root)
entry.pack()

listbox = tk.Listbox(root)
listbox.pack()
def expense():
    a = entry.get()
    if a:
        data.append(a)
        with open ("expenses.josn", "w") as file:
            json.dump(data, file)
        print(f"Added expenses: {a}")
    else:
        print("The line is empty")
        
def delete_expense():
    try:
        chosen_expense = listbox.curselection()
        if chosen_expense:
            expense_to_delete = listbox.get(chosen_expense)
            data.remove(expense_to_delete)
            with open("expenses.json", "w")as file:
                json.dump(data,file)
            listbox.delete(chosen_expense)
            print(f"deleted expense:{expense_to_delete}")
        else:
            print("No expense was chosen")
    except Exception as e:
        print(f"Error:{e}")
        
def edit_expense():
    try:
        chosen_expense = listbox.curselection()
        if chosen_expense:
            expense_to_edit = listbox.get(chosen_expense)
            new_expense = tk.simpledialog.askstring("Edit Expense", f"edit the expense:\n{expense_to_edit}")
            if new_expense:
                index = data.index(expense_to_edit)
                data[index] = new_expense
                with open("expenses.json", "w")as file:
                    json.dump(data, file)
                listbox.delete(chosen_expense)
                listbox.insert(chosen_expense,new_expense)
                print(f"edited expense:{new_expense}")
            else:
                print("No expense was chosen")
    except Exception as e:
        print(f"Error:{e}")
button =tk.Button(root, text="Press", command = expense)
button.pack()

button_delete = tk.Button(root, text="Delete Expense", command = delete_expense)
button_delete.pack()

button_edit = tk.Button(root, text="Edit Expense", command = edit_expense)
button_edit.pack()

root.mainloop()