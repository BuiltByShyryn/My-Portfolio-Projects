from django.urls import path, include
from .views import v_home, v_user_info


urlpatterns = [
    path('', v_home , name = ''),
    #path('contac/', ....),
    path('user_info/',v_user_info.as_view(), name = 'users_info' )
    
]
