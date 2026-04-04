from django.urls import path, include
from .views import *

# mysite.kz/api/...
urlpatterns = [
    # mysite.kz/api/integer/...
    path('integer/', v_api_integer, name = 'api_int'),
    path('name/',    v_api_name,name = 'api_name' ),
    path('iin/',     v_api_iin, name = 'api_iin'),
    
]
