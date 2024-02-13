import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';

class RegisterFailureDialog extends StatelessWidget {
  const RegisterFailureDialog({super.key, required this.onTap});

  final Function() onTap;

  @override
  Widget build(BuildContext context) {
    return MyBottomDialog(
      onTap: onTap,
      title: AppStrings.somethingWentWrong,
      iconWidget: const Icon(
        Icons.close,
        color: Colors.red,
        size: 60,
      ),
    );
  }
}
