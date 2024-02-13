import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';

class MyBottomDialog extends StatelessWidget {
  const MyBottomDialog({
    super.key,
    required this.onTap,
    required this.iconWidget,
    required this.title,
    this.subTitle,
  });

  final Function() onTap;
  final Widget iconWidget;
  final String title;
  final String? subTitle;

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
            child: iconWidget,
          ),
          const SizedBox(height: 8),

          Text(
            title,
            style: AppTextStyles.bodyLargeTextStyle,
          ),

          if (subTitle != null)
            Padding(
              padding: const EdgeInsets.only(top: 8),
              child: Text(
                subTitle!,
                style: AppTextStyles.bodyMediumTextStyle,
              ),
            ),

          const SizedBox(height: 32),

          // btn
          MainButton(
            isEnabled: true,
            text: AppStrings.ok,
            onTap: onTap,
          ),
        ],
      ),
    );
  }
}
