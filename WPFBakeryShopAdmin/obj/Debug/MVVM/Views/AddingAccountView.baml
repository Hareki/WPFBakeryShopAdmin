   M S B A M L   `   `   `  ����M  IWPFBakeryShopAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WPFBakeryShopAdmin.ViewsWPFBakeryShopAdmin.Views  5   	   M+clr-namespace:WPFBakeryShopAdmin.ConvertersWPFBakeryShopAdmin.Converters  5   	   a5clr-namespace:WPFBakeryShopAdmin.LocalValidationRules'WPFBakeryShopAdmin.LocalValidationRules  5	   	   M+clr-namespace:WPFBakeryShopAdmin.ViewModelsWPFBakeryShopAdmin.ViewModels  5
   	   0    *WPFBakeryShopAdmin.Views.AddingAccountView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:WPFBakeryShopAdmin.Views     5   	   _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   ;	converter+clr-namespace:WPFBakeryShopAdmin.Converters  5   	   F
validation5clr-namespace:WPFBakeryShopAdmin.LocalValidationRules  5	   	   <
viewmodels+clr-namespace:WPFBakeryShopAdmin.ViewModels  5
   	   $��15(�5   	     � Title$  Tạo tài khoản mới��5   	    � Icon$# /Resources/Images/favicon.ico��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	      MaterialDesignBody#���   5   	   $	��620��5   	   $	��600��5   	    � ShowInTaskbar .   � 	Resources 5      ;   5WPFBakeryShopAdmin.Converters.LargeUserImageConverter  imageConverterD   >WPFBakeryShopAdmin.Converters.ReverseVisibilityToBoolConverter  reverseVisConverter��%:   &	       &	        5   
   6E     5   
   6S   5      .��5      ��  �� Padding$ 30q�6   $��Center=�5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $��0 �6#   �� 5      $��4* �6#   �� 5       $��0 �6#   .+�5"      � $��1��6   {�5#      �� *LoadingPageVis+ �� 	Converter# [  5$      �� 5%      $��* �6'   �� 5&      $��* �6'   �� 5'      $��* �6'   �� 5(      $��* �6'   �� 5)      $��* �6'   �� 5*      $��* �6'   �� 5+      $��* �6'   �� 5,      $��* �6'   	 � RowDefinitions	 5.      �� 5/      $
��auto �6$   �� 50      $
��auto �6$   �� 51      $
��auto �6$   �� 52      $
��auto �6$   �� 53      $
��auto �6$   �� 54      $
��auto �6$   �� 55      $
��auto �6$   �� 56      $
��auto �6$   .+�59      �� 
 �� Orientation$
 
Horizontal=�6   $��0��5:      $��12��5;      .+�5<      ��  $  MaterialDesignHeadline4TextBlock#��[ 6    $��SemiBold"�5=         TitleMargin#��[ 5>         CreateNewAccount#���  5?       5@      � 5B      $��0��6   $��12��5C      $��Center=�5D      .+�5E      '  !MaterialDesignThemes.Wpf.Snackbar  -   5          Name$ GreenSB��5H   .   $��Center=�5E   .   $	��Top=�5F   .   $��12��5G   .   ���#00b4d8   MessageQueue 5J   .   4  .MaterialDesignThemes.Wpf.MessageQueueExtension     Message. 5K      .  (MaterialDesignThemes.Wpf.SnackbarMessage  .��5L      �� -   5         ��Name$ GreenContent��5M   )   $
 
Horizontal=�5L   )   .+�5N   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ CheckCircle=�6:   $��0 3 10 0q�5O   :   $��Center=�5P   :   �� 5Q   "   -   5         ��Name$ GreenMessage��5Q   ,   $-��'Cập nhật trạng thái thành công��5R   ,   $��SemiBold"�5S   ,   5T      5U      5V        5X      -   5        $ RedSB��5[   .   $��Center=�5X   .   $	��Top=�5Y   .   $��12��5Z   .   ���#e63946 5]   .     . 5^        .��5_      �� -   5        $ 
RedContent��5`   )   $
 
Horizontal=�5_   )   .+�5a   "     $ CheckCircle=�6:   $��0 3 10 0q�5b   :   $��Center=�5c   :   �� 5d   "   -   5        $ 
RedMessage��5d   ,   $%��Xảy ra lỗi khi cập nhật��5e   ,   $
��Bold"�5f   ,   5g      5h      5i      5j      �� 5l      -   5         ��Name$ txtLastName��5l      $��0��5m      $��2��5n      $��4��5o      $��	0 0 15 15q�5p       ! MaterialDesignOutlinedTextBox#��[ 5q        	LastName2)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5r         
Background
 �����.  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton .  .��5t   P   ��5u      �� 5v       �� Path$ PersonalAccount.LastName�6"    �� ValidationRules 5w      D	   >WPFBakeryShopAdmin.LocalValidationRules.NotEmptyValidationRule	  5x   "    	  Message$) #Vui lòng nhập họ và tên lót��6D   5z      .��5{   $   5|      �� 5~      -   5        $ txtFirstName��5~      $��4��5      $��2��5�      $��4��5�      $��	15 0 0 15q�5�      #��[ 5�        
FirstName2# �  5�      
 ����� .  .��5�   P   ��5�      �� 5�      $ PersonalAccount.FirstName�6"    5�      	  5�   "   $ Vui lòng nhập tên��6D   5�      .��5�   $   5�      �� 5�      -	   5        $ txtEmail��5�      $��0��5�      $��4��5�      $��8��5�      $
��0 15q�5�      #��[ 5�       
	 Email2# � 	 5�      
 ����� .  .��5�   P   ��5�      �� 5�      $ PersonalAccount.Email�6"    5�      	  5�   "   $) #Vui lòng nhập địa chỉ email��6D   G
   AWPFBakeryShopAdmin.LocalValidationRules.EmailFormatValidationRule
  5�   "    
  Message$( "Email không đúng định dạng��6G   5�      .��5�   $   5�      �� 5�      -
   5        $ PersonalAccount_Address��5�      $��0��5�      $��3��5�      $��8��5�      $
��0 15q�5�      #��[ 5�       
 Address# � 
 5�      
 ����� .  5�      �� 5�      -   5        $ txtPhone��5�      $��0��5�      $��5��5�      $��5��5�      $��
0 15 15 15q�5�      #��[ 5�       
 Phone2# �  5�      
 ����� .  .��5�   P   ��5�      �� 5�      $ PersonalAccount.Phone�6"    5�      	  5�   "   $+ %Vui lòng nhập số điện thoại��6D   G   AWPFBakeryShopAdmin.LocalValidationRules.PhoneFormatValidationRule  5�   "      Message$7 1Số điện thoại không đúng định dạng��6G   5�      .��5�   $   5�      �� 5�      -   5         ��Name$ LanguageList��5�      $��5��5�      $��5��5�      $��3��5�      $��
15 15 0 15q�5�       " MaterialDesignOutlinedComboBox#��[ 5�        Language# �  5�       �� SelectedItem 5�      �� *SelectedItemLanguage+ �� Mode$ OneWayToSource=� �� SelectedIndex 5�      �� $# PersonalAccount.LanguageIndex���5�      �� 5�      .R�5�      �� $
 
Horizontal=�6)   .+�5�   "   ��  �� BitmapScalingMode$ HighQuality=�5�   (    �� EdgeMode$ Aliased=�5�   (   $��25��5�   (   $��0 0 7 0q�5�   (   ��5�   (   �� *LanguageFlagURL+5�   (   �� 5�   "   ��6,   �� *LanguageName+5�      5�      5�      �� 5�      -   5          ��Name$  RoleList��5�      $��0��5�      $��6��5�      $��8��5�      $
��0 15q�5�       * &MaterialDesignFilterChipPrimaryListBox#��[ 5�      ��5�      �� 5�      .R�5�      �� $
 
Horizontal=�6)   .+�5�   "   �� ��6,   �� *DisplayName+5�      5�      ��5�      �� 5�      ! �� 
TargetType"! ��6     . *MaterialDesignFilterChipPrimaryListBoxItem" �� BasedOn#" [ 5�       # �� Triggers# 5�      �� 5�   "   $ �� Binding$ 6.   �� *IsLockedRole+% �� Value% True5�   .   .a�5�   &   �� & �� Property& � {�' �� Value' .  5�   -   �� 5�   &   & �@��
IsSelected' . 5�   -   5�   #   .�5�      �� & � ��$' 0 0 15 0q�5�   %   5�      5�      �� 5�      $
��0 15q�6   $��0��5�      $��10��5�      $��7��5�      $
 
Horizontal=�5�      $��RightToLeft=�5      .+�5     �� -   5        ( ��Name$( CancelAdding��5       CustomSecondaryButton#��[ 5     +  %MaterialDesignThemes.Wpf.ShadowAssist)   ShadowDepth$) Depth0=�5      
 Cancel#���  5     $��0 0 20 0q�5     �� 5     -   5        $( AddNewAccount��5       CustomPrimaryButton#��[ 5	       CreateAccount#���  5
     $) Depth0=�5     5     5     5     5     5     