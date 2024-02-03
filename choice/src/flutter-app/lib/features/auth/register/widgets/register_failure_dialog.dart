import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';

class RegisterFailureDialog extends StatelessWidget {
  const RegisterFailureDialog({super.key, required this.onTap});

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
          const SizedBox(
              height: 60,
              width: 60,
              child: Icon(
                Icons.close,
                color: Colors.red,
                size: 60,
              )),
          const SizedBox(height: 8),

          Text(
            'Кажется, что-то пошло не так...',
            style: AppTextStyles.bodyLargeTextStyle,
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
