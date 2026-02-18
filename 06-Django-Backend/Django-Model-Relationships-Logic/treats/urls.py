
 
from django.urls import path
from .views import view_child_form,view_human_form,view_icecream_form,\
view_kiosk_form,view_home

urlpatterns = [
    path('human/add/', view_human_form, name='human_form'),
    path('child/add/', view_child_form, name='child_form'),
    path('icecream/add/', view_icecream_form, name='icecream_form'),
    path('kiosk/add/', view_kiosk_form, name='kiosk_form'),
    path('', view_home, name='home'),
]
