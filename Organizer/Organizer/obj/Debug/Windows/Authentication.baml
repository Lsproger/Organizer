   M S B A M L   `   `   `  ����D  @Organizer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Organizer	Organizer  5   	       Organizer.Authentication   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Organizer     5   	   -   5          �Name$  W_Authentication��5   	    � Title$ Authentication��5   	   $	��200��6    $	��300��6-   $	��300��69   $	��200��6H   $	��300��6X   $	��200��6g    � WindowStartupLocation$ CenterScreen=�6w    � WindowStyle$ 
ToolWindow=�6�   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $	��10* �6   �� 5      $��2* �6   �� 5      $	��15* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      $��30 �6   �� 5      $��10 �6   �� 5      $��30 �6   �� 5      $��10 �6   �� 5      $��20 �6   �� 5      $��10 �6   �� 5      $��30 �6   �� 5      $��10 �6   .+�5   
   �� -   5         ��Name$ 
_authLogin��5      $��1��6&   $��5��63   $
��10,0q�6G   ���	#FFCFCFCF$��Center=�6m   $	��5,0q�6�    � ToolTip$ Login��6�   4  .System.Windows.Automation.AutomationProperties   Name$ login_field��6�   G� 5   
   -   5        	 G�Name$	 _authPassword��5      $��3��6-   $��5��6:   $
��10,0q�6N   $��Center=�6\   $	��5,0q�6~   $ Password��6�   $ password_field��6�   �� 5   
   -   5        
 ��Name$
 _rememberUser��5       $��5��6   $��5��67   $
��10,0q�6K   $��Center=�6Y   $ remember_me_checkbox��6{   $8 2Check if you would like to authorize automatically��6�   .��6�   +)Входить автоматически5       �� 5!   
   -   5         ��Name$ _log_in��5!      $��7��6"   $��1��6/   $��
Войти��6?   $��0q�6O   
������$ _log_in��6�    �� 	IsDefault . 6�   �� 5"   
   -   5        $ _gotoRegistration��5"      $��7��6,   $��3��69   $��Регистрация��6I   $��0q�6_   ���	#FFECC1C1$ _gotoRegistration��6�   5$      5%      