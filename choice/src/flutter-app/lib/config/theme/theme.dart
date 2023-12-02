
import 'package:choice/config/theme/colors.dart';
import 'package:flutter/material.dart';

class AppTheme {

  static get lightTheme => ThemeData(
    useMaterial3: true,
    progressIndicatorTheme: ProgressIndicatorThemeData(
      color: AppColors.linearLoaderBackgroundColor,
      linearTrackColor: AppColors.linearLoaderColor,
      linearMinHeight: 1.3,
    ),
  );

  static get darkTheme => ThemeData(
    useMaterial3: true,
    brightness: Brightness.dark,
  );

}