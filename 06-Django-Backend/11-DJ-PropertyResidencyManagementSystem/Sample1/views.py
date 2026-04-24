from django.shortcuts import render
from django.http.request import HttpRequest

from django.http.response import HttpResponse, Http404,HttpResponseNotFound
from .models import Cities, Street,House
# Create your views here.

def home(req: HttpRequest) -> HttpResponse:
    lst_citites = Cities.objects.all()
    lst_streets = Street.objects.all()
    lst_house = House.objects.all()
    resp = render(req, template_name='home.html', 
                  context={
                      'ls_city': lst_citites,
                      'ls_street' : lst_streets, 
                      'ls_house': lst_house,
                  })
    return resp
    