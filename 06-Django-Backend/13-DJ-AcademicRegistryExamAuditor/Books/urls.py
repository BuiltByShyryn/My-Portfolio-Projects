

from django.urls import path,include
from .views import  cv_home
from django.contrib.auth.views import LoginView, LogoutView
from django.contrib.auth import views as auth_views
from . import views

urlpatterns = [
    
    path('', cv_home.as_view(), name = 'home'),
    path('login/', LoginView.as_view(), name = 'my_login'),
    path('logout/', LogoutView.as_view(), name = 'my_logout'),
    path('register/', views.register, name='register'),

    path('password_change/', auth_views.PasswordChangeView.as_view(template_name='password_change.html'), name='password_change'),
    path('password_change/done/', auth_views.PasswordChangeDoneView.as_view(template_name='password_change_done.html'), name='password_change_done'),

    
    path('password_reset/', auth_views.PasswordResetView.as_view(template_name='password_reset.html'), name='password_reset'),
    path('password_reset/done/', auth_views.PasswordResetDoneView.as_view(template_name='password_reset_done.html'), name='password_reset_done'),
    path('reset/<uidb64>/<token>/', auth_views.PasswordResetConfirmView.as_view(template_name='password_reset_confirm.html'), name='password_reset_confirm'),
    path('reset/done/', auth_views.PasswordResetDoneView.as_view(template_name='password_reset_complete.html'), name='password_reset_complete'),
    
]
