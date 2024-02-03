import 'package:auto_route/auto_route.dart';
import 'package:choice/features/entry_point/bloc/auth_bloc.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class RegisterFinishedDialog extends StatelessWidget {
  const RegisterFinishedDialog({super.key, required this.onTap});

  final Function() onTap;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 254,
      margin: const EdgeInsets.all(8),
      padding: const EdgeInsets.only(bottom: 4),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(18),
        color: Colors.white,
      ),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.end,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          const SizedBox(height: 8),

          // icon
          SizedBox(
            height: 60,
            width: 60,
            child: SvgPicture.asset(
              'assets/svg/ok_icon.svg',
              fit: BoxFit.fitWidth,
            ),
          ),
          const SizedBox(height: 8),

          Text(
            AppStrings.accountIsCreated,
            style: AppTextStyles.bodyLargeTextStyle,
          ),
          const SizedBox(height: 8),

          Text(
            AppStrings.nowYouCanCreateAccounts,
            style: AppTextStyles.bodyMediumTextStyle,
          ),

          const SizedBox(height: 32),

          // btn
          MainButton(
            isEnabled: true,
            text: 'Ok',
            onTap: onTap,
          ),
        ],
      ),
    );
  }
}
