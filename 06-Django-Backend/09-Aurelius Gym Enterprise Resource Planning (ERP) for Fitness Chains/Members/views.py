from django.shortcuts import render, get_object_or_404, redirect
from .models import Member, GymBranch, MembershipPlan
# Create your views here.
from django.db.models import Q
from django.views.generic import TemplateView
from django.http import HttpRequest, HttpResponse
from .forms import MemberForm,LoginForm,CreateUserForm
from django.contrib.auth import login, authenticate
from django.contrib import messages 
from .forms import MembershipPlanForm
from django.core.paginator import Paginator
from datetime import date

class VMembers(TemplateView):
    template_name = 'member_list.html'

    def get(self, req: HttpRequest):
        search_query = req.GET.get('search', '')

        if search_query:
            parts = search_query.split()
            members = Member.objects.filter(
                Q(iin__icontains=search_query) |
                Q(first_name__icontains=search_query) | 
                Q(last_name__icontains=search_query) |
            (
                Q(first_name__icontains=parts[0]) & 
                Q(last_name__icontains=parts[-1])
                )
            ).order_by('-membership_start')
        else:
            members = Member.objects.all().order_by('-membership_start')

        paginator = Paginator(members, 10)  
        page_number = req.GET.get('page')
        page_obj = paginator.get_page(page_number)

        
        for member in page_obj:
            member.exp_date = member.expiration_date()

        return self.render_to_response({
            'title': 'List of members',
            'data': page_obj,
            'search_query': search_query,
        })
        
        
class home_page(TemplateView): 
    
    template_name = 'index.html'
    
    def __init__(self, **kwargs):
        super().__init__(**kwargs)
        
    def get(self, req): 
        total_members = Member.objects.count()
        total_branches = GymBranch.objects.count()
        total_plans = MembershipPlan.objects.count()
        
        context = {
            "Title": 'Gym Management Portal',
            'Text': "Welcome to the Gym Portal",
            "m_count": total_members,
            "b_count": total_branches,
            "p_count": total_plans,
        }
        return render(req, self.template_name, context)
        
class VMemberDetail(TemplateView):
    template_name = "member_detail.html"

    def get(self, req, pk):
        member = get_object_or_404(Member, pk=pk)
        return self.render_to_response({
            'member': member,
            'title': f"Profile: {member.first_name}",
            'today': date.today(),
            'expiration_date': member.expiration_date()
        })
        
def member_add_view(req):
    if req.method == "POST":
        form = MemberForm(req.POST, req.FILES)  
        if form.is_valid():
            member = form.save(commit=False)
            member.membership_duration_months = member.plan.duration_months
            member.save()

            messages.success(req, "Member added successfully! ")
            return redirect('member_list')
    else:
        form = MemberForm()

    return render(req, 'member_form.html', {
        'title': "The Gym service",
        'form': form
    })
    
def member_delete_view(req,pk):
    member = get_object_or_404(Member, pk = pk)
    if req.method == "POST": 
        member.delete()
        return redirect('member_list')
    return render(req, 'member_delete.html', {
        'title': "Deleting Page",
        'member' : member
    })
    
def member_edit_view(req,pk): 
    member = get_object_or_404(Member, pk=pk )
    
    if req.method =="POST": 
        form = MemberForm(req.POST, instance = member)
        if form.is_valid(): 
            form.save()
            messages.success(req, f'Updated {member.first_name} successfully!')
            return redirect('member_list')
    else: 
        form = MemberForm(instance = member)
        
    return render(req, 'member_form.html', {
        'title': "Edit Member",
        'form': form
    })
    
    
def my_login(req: HttpRequest): 
    error_message = None 
    
    if req.method == "POST":
        form = LoginForm(req.POST)
        if form.is_valid():
            user = authenticate(
                username=form.cleaned_data['username'], 
                password=form.cleaned_data['password']
            )
            if user is not None:
                login(req, user)
                return redirect('index')
            else:
               
                error_message = "Invalid username or password! Try again."
    else:
        form = LoginForm()
    
   
    return render(req, 'registration/login.html', {
        'form': form, 
        'error': error_message
    })
def create_user(req:HttpRequest)-> HttpResponse:
    if req.method =="POST": 
        form =CreateUserForm(req.POST)
        if form.is_valid(): 
            new_user = form.save(commit = False)
            new_user.set_password(form.cleaned_data['password'])
            new_user.save()
            
            return render(req, 'registration/create_user_ok.html', {
                'new_user_name': new_user.username,
                'id_new_user': new_user.pk
            }) 
    else:
        form  = CreateUserForm()
    return render(req, 'registration/create_user.html', {'form': form})
    
def plan_list_view(req): 
    
    all_plans = MembershipPlan.objects.all()
    
    context = {
        'plans': all_plans,
        'title': 'Gym Membership Plans'
    } 
    
    return render(req, 'plan_list.html', context)

def plan_add_view(request):
    if request.method == "POST":
        form = MembershipPlanForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request, "New Plan added to the catalog! 💳")
            return redirect('plan_list')
    else:
        form = MembershipPlanForm()
    
    return render(request, 'plan_add.html', {'form': form, 'title': 'Create New Plan'})

def plan_delete_view(request, pk):
    plan = get_object_or_404(MembershipPlan, pk=pk)
    if request.method == "POST":
        plan.delete()
        return redirect('plan_list')
   
    return render(request, 'plan_confirm_delete.html', {'plan': plan})



def branch_list(request):
    branches = GymBranch.objects.all()
    return render(request, 'branches.html', {'branches': branches})

