from django.shortcuts import render
from .models import Assignment

def assignment_list(request):
    assignments = Assignment.objects.all()
    return render(request, 'assignment/assignment_list.html', {'assignments': assignments})
# Create your views here.
