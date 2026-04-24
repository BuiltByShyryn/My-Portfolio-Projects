
 
from django.urls import path
from .views import view_home, view_brands, view_brand_info,\
    view_brand_form,view_customers, view_customer_info,\
        view_good_info,view_goods,view_customer_form,\
            view_goods_form, view_brand_search,view_cutomer_search
from .view_class import ViewBrandForm, ViewGoodsForm
urlpatterns = [
    path('', view_home, name = "home"),
    path('brand/', view_brands, name = "brand"),
    path('brand_info/<int:id>/', view_brand_info, name = "brand_info"),
    path('brand_form/', view_brand_form, name = 'brand_form'),
    
    path('brand_form2/', ViewBrandForm.as_view(), name = 'brand_form2'),
    
    path('customers/', view_customers, name = "customers"),
    path('customer_info/<int:id>/', view_customer_info, name = 'customer_info'),
    path('goods/',view_goods, name='goods' ),
    path('good_info/<int:id>/', view_good_info, name='good_info'),
    path('customer_form/', view_customer_form, name='customer_form'),
    path('goods_form/', view_goods_form, name = "goods_form"),
    
    path('goods_form2/', ViewGoodsForm.as_view(), name = "goods_form2"),
    
    
    
    path('brand_search/', view_brand_search, name = 'brand_search'),
    path('customer_search/', view_cutomer_search, name = 'customer_search'),
]
