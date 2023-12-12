import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:flutter/cupertino.dart';

class CreateAccountSheet extends StatelessWidget {
  const CreateAccountSheet({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return CupertinoActionSheet(
      actions: [
        CupertinoActionSheetAction(
          isDefaultAction: true,
          onPressed: () {
            // sign up as client
          },
          child: Text(
            AppStrings.createClientAccountText,
            style: AppTextStyles.actionSheetTextStyle,
          ),
        ),
        CupertinoActionSheetAction(
          isDefaultAction: true,
          onPressed: () {
            // sign up as company
          },
          child: Text(
            AppStrings.createCompanyAccountText,
            style: AppTextStyles.actionSheetTextStyle,
          ),
        ),
      ],
      cancelButton: CupertinoActionSheetAction(
        isDefaultAction: true,
        onPressed: () => Navigator.pop(context),
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
