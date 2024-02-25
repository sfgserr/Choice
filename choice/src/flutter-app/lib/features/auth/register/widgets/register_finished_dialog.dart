import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class RegisterFinishedDialog extends StatelessWidget {
  const RegisterFinishedDialog({super.key, required this.onTap});

  final Function() onTap;

  @override
  Widget build(BuildContext context) {
    return MyBottomDialog(
      onTap: onTap,
      iconWidget: SvgPicture.asset(
        'assets/svg/ok_icon.svg',
        fit: BoxFit.fitWidth,
      ),
      title: AppStrings.accountIsCreated,
      subTitle: AppStrings.nowYouCanCreateAccounts,
    );
  }
}
