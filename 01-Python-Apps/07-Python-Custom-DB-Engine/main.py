#Path to db
FILENAME = 'users.txt'

def read_users():
    #Read Users from the  db 
    try:
        with open(FILENAME, "r") as f:
            for line in f:  
                if line:
                    id,name,age = line.split(';')
                    users.append({'id': int(id), "name": name, "age": int(age)})
    except FileNotFoundError:
        with open(FILENAME, "w"):
            pass
    except Exception as e: 
        print("Error", e)
    return users   

def save_users(users):
    try:
        with open(FILENAME,"w") as f:
            for user in users:
                f.write(f"{user['id']};{user['name']}; {user['age']}\n")
    except Exception as e:
        print("Error during the file creation", e )
        
        
def add_user(name, age):
    if not name: 
        raise ValueError("Your name cant be empty ")
    users = read_users()
    
    ids = []
    for user in users: 
        ids.append(user['id'])
        
        
        newId = max([u["id"] for u  in users], default =0)+1
        users.append({"id": newId, "name":name, "age": age})
        save_users(users)
        print("New user was added: ", name)
        
def get_users():
    return read_users()
def get_user_id(id):
    users = read_users()
    for user in users:
        if user['id']== id:
            return user
    return None   
def delete_user(id):
    users = read_users()
    new_users = []
    for user in users:
        if user['id']!=id:
            new_users.append(user)
    save_users(new_users)
    print(f"User {id} is deleted")
def get_adults():
    # >18
    users = read_users()
    adults = []
    for user in users:
        if user['age']>18:
            adults.append(user)
        return adults
        
   

def update_age(id, new_Age):
    users = get_users()
    for user in users: 
        if user[id] == id:
            user['age'] = new_age
    save_users(users)
    print(f"User {id} updated the age  to the {new_age}")    
def search_by_name(searchQueryFunc):
   users = read_users()
   results = []
   for user in users:
       if searchQueryFunc(user['name']):
           results.append(user)
   return results

add_user("Shyryn", 22)

    