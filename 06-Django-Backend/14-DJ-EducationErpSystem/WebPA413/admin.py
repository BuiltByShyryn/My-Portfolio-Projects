from django.contrib import admin
from .models import Course, Group, Student, Exam
# Register your models here.
admin.site.register([Course,Group,Student,Exam])