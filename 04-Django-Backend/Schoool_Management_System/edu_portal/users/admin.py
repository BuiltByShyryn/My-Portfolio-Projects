from django.contrib import admin
from edu_portal.assignments import models


# Register your models here.


class User(models.Model):
    name = models.CharField(max_length=100)
    age = models.IntegerField()
    email = models.EmailField()

    def __str__(self):
        return self.name
