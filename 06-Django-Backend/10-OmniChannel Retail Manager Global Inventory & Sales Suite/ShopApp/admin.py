from django.contrib import admin
from .models import model_list
import threading as th
#from threading import  Timer

# Register your models here.
admin.site.register( model_list )


cnt_brands = 0
def MyTimer(*argv, **kwarg): 
    from .models import Brand
    print('args=', argv)
    print('kwarg=',kwarg)
    global cnt_brands
    #brands : Brand = model_list [0]
    new_cnt_brands = Brand.objects.all().count()
    if cnt_brands !=  Brand.objects.all().count(): 
        new_cnt_brands = Brand.objects.all().count()
        print(f'Changed off brands count {cnt_brands} =>{new_cnt_brands}')
        
        cnt_brands = new_cnt_brands
    pass
    return my_thread

pass
period = 2.0
my_thread = th.Timer(2.0, MyTimer, args=[123, '456'])

th.Timer(5.0, my_thread.start).start()