from django.contrib.auth.forms import AuthenticationForm,UserCreationForm
from django import forms
#from django.contrib.auth.views import LoginView, LogoutView
from django.contrib.auth.models import User

class Login(AuthenticationForm):
    username =forms.CharField(max_length = 100, label = "User Name",
                              widget = forms.TextInput(attrs = {
                                  'placeholder': "Enter user name"
                              })
                              )
    password  = forms.CharField(max_length=100, label = "Password", 
                                widget = forms.PasswordInput(
                                    attrs={
                                        'placeholder': "Enter password"
                                    }
                                ))
    
    class Meta:
        model = User
        fields = (
            'username', 'password'
        ) 
        labels = {
            'username': "User Name2",
            'password': "Password"
        }


 
class CreateUser(UserCreationForm): 
    #username 
    #email
    #first_name 
    #last_name
    #password1
    #password2
    
    class Meta:
        model = User
        fields = '__all__'
        fields = ('username', "first_name", 'password1', 'password2')