import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';

class AppLogoWidget extends StatelessWidget {
  const AppLogoWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 110,
      height: 110,
      child: Image.asset(AppImages.logoPath, fit: BoxFit.cover),
    );
  }
}
