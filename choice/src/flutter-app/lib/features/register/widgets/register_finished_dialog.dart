import 'package:choice/ui/ui.dart';
import 'package:flutter/cupertino.dart';

class RegisterFinishedDialog extends StatelessWidget {
  const RegisterFinishedDialog({super.key});

  @override
  Widget build(BuildContext context) {
    return CupertinoActionSheet(
      actions: [
        CupertinoActionSheetAction(
          isDefaultAction: true,
          onPressed: () {},
          child: Text(
            AppStrings.createClientAccountText,
            style: AppTextStyles.actionSheetTextStyle,
          ),
        ),
      ],
      cancelButton: CupertinoActionSheetAction(
        isDefaultAction: true,
        onPressed: () => Navigator.of(context).pop(),
        child: Text(
          AppStrings.cancelText,
          style: AppTextStyles.actionSheetTextStyle.copyWith(
            fontWeight: FontWeight.w500,
          ),
        ),
      ),
    );
  }
}
