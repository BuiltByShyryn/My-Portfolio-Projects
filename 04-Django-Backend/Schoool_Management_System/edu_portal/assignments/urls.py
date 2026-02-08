from django.urls import path
from .models import Assignment

urlpatterns = [
    path('', Assignment, name='assignments/assignment_list'),

]