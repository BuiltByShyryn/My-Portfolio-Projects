from django.shortcuts import render
from django.http import HttpRequest,HttpResponse,HttpResponseBadRequest
from .models import Human, Child, Ice_Cream, Kiosk
from .forms import HumanForm, ChildForm, IceCreamForm, KioskForm


def view_home(req: HttpRequest) -> HttpResponse:
    return render(req, 'home.html')

    
def view_human_form(req: HttpRequest) -> HttpResponse:
    if req.method == 'GET':
        return render(req, 'generic_form.html', {'form': HumanForm(), 'title': 'Add Human'})
    
    elif req.method == 'POST':
        form = HumanForm(req.POST)
        if form.is_valid():
            obj = Human()
            obj.name = form.cleaned_data['name']
            obj.age = form.cleaned_data['age']
            obj.save()
            return render(req, 'generic_form.html', {'form': HumanForm(), 'status': 'Human saved!', 'title': 'Add Human'})
       
        return render(req, 'generic_form.html', {'form': form, 'status': 'Error!', 'title': 'Add Human'})
    
def view_child_form(req: HttpRequest)-> HttpResponse:
    if req.method == "GET": 
         return render(req, 'generic_form.html', {'form': ChildForm(), 'title': 'Add Child'})
    elif req.method =="POST":
        form = ChildForm(req.POST)
        if form.is_valid():
            obj = Child()
            obj.name = form.cleaned_data['name']
            obj.age = form.cleaned_data['age']
            obj.parent = form. cleaned_data['parent']
            obj.save()
            return render(req, 'generic_form.html', {'form': ChildForm(), 'status': 'Child saved!', 'title': 'Add Child'})
    return render(req,'generic_form.html', {'form': form, 'status': 'Error!', 'title': 'Add Human'})
    
def view_icecream_form(req: HttpRequest)-> HttpResponse:
    if req.method == "GET": 
        return render(req, 'generic_form.html', {'form': IceCreamForm(), 'title': 'Add Ice Cream'})
    elif req.method =="POST":
        form = IceCreamForm(req.POST)
        if form.is_valid():
            obj = Ice_Cream()
            obj.name = form.cleaned_data['name']
            obj.desc = form.cleaned_data['desc']
            obj.save()
            return render(req, 'generic_form.html', {'form': IceCreamForm(), 'status': 'Ice Cream saved!', 'title': 'Add Ice Cream'})
    return render(req,'generic_form.html', {'form': form, 'status': 'Error!', 'title': 'Add Ice Cream'})
   
def view_kiosk_form(req: HttpRequest)->HttpResponse:
    if req.method == "GET": 
        return render(req, 'generic_form.html', {'form': KioskForm(), 'title': 'Add Kiosk'})
    elif req.method =="POST":
        form = KioskForm(req.POST)
        if form.is_valid():
            obj = Kiosk()
            obj.name = form.cleaned_data['name']
            obj.ice_cream_type = form.cleaned_data['ice_cream_type']
            obj.address = form.cleaned_data['address']
            obj.save()
            return render(req, 'generic_form.html', {'form': KioskForm(), 'status': 'kiosk saved!', 'title': 'Add Kiosk'})
    return render(req,'generic_form.html', {'form': form, 'status': 'Error!', 'title': 'Add Kiosk'})
