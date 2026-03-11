#task class 1
class User:
    def __init__(self, name, email, password, weight,height):
        self.__name = name
        self.__email = email
        self.__password = password 
        self.__weight = weight 
        self.__height = height 
    def set_name(self, name):
        self.__name = name 
    def set_email(self,email):
        self.__email = email
    def set_password(self,password):
        self.__password = password
    def set_weight(self,weight):
        self.__weight = weight 
    def set_height(self,height):
        self.__height= height 
    def get_name(self,name):
        return self.__name
    def get_email(self, email):
        return self.__email
    def get_password(self,password):
        return self.__password
    def get_weight(self,weight):
        return self.__weight
    def get_height(self, height):
        return self.__height 
    def  display_info(self):
        return f"name:{self.__name}, email: {self.__email},weight:{self.__weight}kg,height:{self.__height}cm"
#class 2 
class Workout:
    def __init__(self, type_workout, duration):
        self.__type_workout = type_workout
        self.__duration = duration 
    def set_duration(self,duration):
        self.__duration = duration
    def get_duration(self):
        return self.__duration
    def get_type_workout(self):
        return self.__type_workout
    def set_type_workout (self, type_workout):
        self.__type_workout = type_workout
    def get_info(self):
        hours = self.__duration // 60
        minutes = self.__duration % 60
        return f"the Name of the Workout {self.__type_workout}, duration:{self.__duration}, hours: {hours}, minutes: {minutes}"
#class 3 
class WorkoutLog:
    def __init__(self):
        self.__workouts = []
    def add_workout(self,workout):
        self.__workouts.append(workout)
    def remove_workout(self, index):
        if 0 <= index < len(self.__workouts):
            self.__workouts.pop(index)
        else:
            print("Wrong index")
    def get_total_duration(self):
        total_duration = 0 
        for workout in self.__workouts:
            total_duration += workout.get_duration()
        return total_duration 
    def get_total_duration_by_type(self,type_workout):
        total_duration = 0
        for workout in self.__workouts:        
            if workout.get_type_workout() == type_workout:
                total_duration += workout.get_duration()
        return total_duration
#task4
class FitnessAccount(User,WorkoutLog):
    def __init__(self,User,Workout_log):
        User.__init__(self, User.get_name(), User.get_email(), User.get_password(), User.get_weight(), User.get_height())
        WorkoutLog.__init__(self)
        self.__User = User 
        
    def add_workout(self, workout):
        WorkoutLog.add_workout(self, workout)
    
    def remove_workout(self, index):
        WorkoutLog.remove_workout(self, index)
    
    def get_user_info(self):
        return self.__User.display_info()
    
    def get_total_workout_time(self):
        return self.get_total_duration()