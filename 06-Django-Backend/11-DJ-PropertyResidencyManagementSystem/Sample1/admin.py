from django.contrib import admin
from .models import Cities, Street, House

# Change admin.register to admin.site.register
admin.site.register(Cities)
admin.site.register(Street)
admin.site.register(House)