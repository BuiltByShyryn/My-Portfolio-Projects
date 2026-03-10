from django.shortcuts import render
from datetime import datetime,timedelta


# Create your views here.

def show_time(request):
    now = datetime.now()
    return render(request, 'time.html',{'current_time':now})


def show_table(request): 
    numbers = range(1,11)
    return render(request, 'table.html', {'numbers': numbers})

def programmer_day(request):
    year = datetime.now().year
    the_date = datetime(year, 1, 1) + timedelta(days=255)
    return render(request, 'programmer.html', {'result': the_date})
    