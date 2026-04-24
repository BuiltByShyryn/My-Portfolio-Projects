from django.shortcuts import render, redirect, resolve_url
from django.urls import resolve
from django.views.generic import TemplateView
# Create your views here.
from django.contrib.auth import login, logout, authenticate
from .forms import Login, CreateUser
from django.contrib.auth.models import User
from django.contrib import messages
from django.http import HttpRequest, HttpResponse
from django.contrib.auth.forms import UserCreationForm
class cv_home(TemplateView):
    
    template_name = 'index.html' #home.html 
    
    def __init__(self, **kwargs):
        super().__init__(**kwargs)

    
    def get(self, request):
        return  self.render_to_response( {
            'Title' :'Home Page', 
            'Text' : "Welcome to this page ",
        })
        
        
def my_login(req:  HttpRequest)-> HttpRequest: 
    if req.method == "GET" : 
        render(req, 'registration/login.html', {'form' : Login()})
    else :
        
        form = Login(req.POST)
        if form.is_valid():
            form.save()
            return redirect('home')
        return render(req, 'registration/login.html', {'form' : form})

    pass
    render(req, 'registration/login.html', {'form' : Login()})
    
def  create_user( req: HttpRequest) -> HttpResponse:
    if req.method =='GET':
        return render(req, 'registration/create_user.html', 
                      {'form' : CreateUser()})

        pass
    else:
        form = CreateUser(req.POST)
        if form.is_valid():
            form.save() #save to model ===> User
            new_user : User= form.instance #take inner model of the form
            return render(req, 'registration/create_user_ok.html', 
                          {'new_user_name' : form.username,
                           'id_new_user': new_user.pk})

        
        return render(req, 'registration/create_user.html', 
                      {'form' : CreateUser()})
        pass
    return render(req, 'registration/create_user.html', 
                      {'form' : CreateUser()})
    pass

def register(request):
    if request.method == "POST":
        form = UserCreationForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request, "Аккаунт создан!")
            return redirect('login')
    else:
        form = UserCreationForm()
    return render(request, 'register.html', {'form': form})