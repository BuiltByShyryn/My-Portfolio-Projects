from django.db import models

# Create your models here.
 
class Cities(models.Model):
    
    name = models.CharField(max_length=100, verbose_name='The City Name',
                            unique=True,
                            db_index=True,
                            )
    info = models.TextField(verbose_name="Information about the city")
 
    
def __str__(self):
    return f'{self.name}'

class  Meta:
    #db_table = ''- name of the tablet inthe DB
    managed = True
    verbose_name = 'City'
    verbose_name_plural = 'Cities'
pass

class Street(models.Model):
    name = models.CharField(max_length=120,
                            verbose_name='The Street Name')
    id_city = models.ForeignKey(Cities, on_delete=models.CASCADE,
                                to_field='id',
                                verbose_name='City')
 
 
    info = models.TextField(verbose_name="Information of the street")
    def __str__(self):
         return self.name
     
    class  Meta:
        #db_table = ''
        managed = True
        verbose_name = 'Street'
        verbose_name_plural = 'Streets'

        
    pass


class House(models.Model):
    number = models.PositiveIntegerField(verbose_name="House Number")
    id_street = models.ForeignKey('Street', on_delete=models.CASCADE,
                            db_index=True      )
    
    def __str__(self):
        return f'{self.number}'
    
    class Meta:
        #db_table = ''
        managed = True
        verbose_name = 'House'
        verbose_name_plural = 'Houses'
        unique_together = [('id_street','number'),]
      #  indexes = [('id_street','number'), ]
    pass
class Person(models.Model):
    
    
    pass 

listModels =[Cities,Street, House]

#'''Экзаменационное задание N 01 

#Таблицы      Пояснения 
#Cities      Города (ID, название города, и т.д.) 
#Streets     Улицы (ID, название улицы, ID-города, и т.д.) 
#Houses    Дома (ID, номер дома, ID-улицы, и т.д.) 
#Flats     Квартиры (ID, номер квартиры, ID-дома, и т.д.) 
#Persons   Владельцы квартир, квартиросъемщики 


#(владелец, квартира, реквизиты владельца/квартиросъемщика, владелец или 
#квартиросъемщик, дата покупки или аренды, арендная плата и т.д.) 
#Владелец может владеть несколькими квартирами, съемщик может снимать несколько квартир. 
##Реализуйте установку арендной платы за снимаемую квартиру 