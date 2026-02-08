from django.shortcuts import render, redirect
from .models import User
from django.contrib.auth import authenticate, logout
from django.contrib.auth.forms import UserCreationForm
from django.contrib.auth.decorators import login_required
def register(request):
    if request.method == 'POST':

        username = request.POST.get('username')
        password = request.POST.get('password')
        email = request.POST.get('email')
        role = request.POST.get('role')


        user = User.objects.create_user(
            username=username,
            password=password,
            email=email,
            role=role
        )
        return redirect("login")


    return render(request, 'register.html')

def login(request):
    if request.method == 'POST':
        username = request.POST.get['username']
        password = request.POST.get['password']
        user = authenticate(
            request,
            username=username,
            password=password
        )
        if user:
            login(request, user)
            return redirect("course_list")
        return redirect(request, "user/login.html")

    @login_required
    def course_list(request):
        logout(request)
        return render(request, 'user/login.html')