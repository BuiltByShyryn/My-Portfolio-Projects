from django.urls import path 
from django.conf import settings
from . import views
from django.conf.urls.static import static
from django.contrib.auth import views as auth_views
from .views import VMembers, home_page, VMemberDetail, member_add_view, member_delete_view,member_edit_view,\
    my_login, create_user, plan_list_view,plan_add_view,plan_delete_view,branch_list
urlpatterns = [
    path('members/', VMembers.as_view() , name = 'member_list'),
    path('',home_page.as_view(), name = 'index'), 
    path('member/<int:pk>/', VMemberDetail.as_view(), name = 'member_detail'),
    path('add/', member_add_view, name = 'member_add'),
    path('member/<int:pk>/delete/', member_delete_view, name = 'member_delete'),
    path('member/<int:pk>/edit/', member_edit_view, name = 'member_edit'),
    path('login/', my_login, name = 'login'),
    path('signup/', create_user, name = 'signup'),
    path('logout/', auth_views.LogoutView.as_view(next_page='index'), name='logout'),
    path('plans/', plan_list_view, name='plan_list'),
    path('plans/add/', plan_add_view, name='plan_add'),
    path('plans/delete/<int:pk>/', plan_delete_view, name='plan_delete'),
    path('branches/', branch_list, name='branch_list'),
]


if settings.DEBUG:
    urlpatterns += static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)