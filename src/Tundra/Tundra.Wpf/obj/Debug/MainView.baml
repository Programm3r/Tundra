   M S B A M L   `   `   `  ����  
Tundra.Wpf    Tundra.Wpf.MainView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  MainView��5       � DataContext 5	      �� *MainViewModel+   Bootstrapper �� Source# [  .��5      � 
���   �.+�5   
   �  � RowDefinitions 5      �� 5      $��1* �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��1* �6    .+�5      �� $��30(�6   $��1��6&   
���� ����5      �� *PersonModel.FullName+ �� Mode$ TwoWay=��� 5      $��30(�6   $��2��6&   
��� � ���6E   �� *PersonModel.Age+$ TwoWay=��� 5      $��3��6   
����� �$��25(�5   *   ��5   %   �� *Foo+5   *   �� 5      $��4��6   $��Registration��6"   
����  �$��30��5      $	��100��6$   ��5      �� *PerformRegistration+5      5      5      