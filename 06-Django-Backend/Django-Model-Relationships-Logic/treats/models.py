from django.db import models
#Человек, Ребёнок, Мороженное, Киоск с мороженным
# Create your models here.

class Human(models.Model):
    name = models.CharField(max_length=255,blank=False, null=False)
    age = models.IntegerField()
    
    def __str__(self):
        return self.name 
        
class Child(models.Model): 
    name = models.CharField(max_length=255,blank=False, null=False)
    age = models.IntegerField(blank=False, null=False)
    parent = models.ForeignKey(Human,on_delete=models.CASCADE)
   
    def __str__(self):
        return self.name 
    
class Ice_Cream(models.Model):
    name = models.CharField(max_length=255,blank=False, null=False)
    desc = models.TextField(max_length=1000)
    def __str__(self):
        return self.name 
class Kiosk(models.Model):
    name = models.CharField(max_length=255,blank=False, null=False)
    ice_cream_type = models.ForeignKey(Ice_Cream, on_delete=models.CASCADE)
    address = models.CharField(max_length=255,blank=True, null=True)
   
    def __str__(self):
        return self.name 