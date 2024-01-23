import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/router.dart';
import 'package:choice/ui/ui.dart';
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
            // sign up as a client
            AutoRouter.of(context).popAndPush(
              RegisterRoute(isCompanyRegister: false),
            );
          },
          child: Text(
            AppStrings.createClientAccountText,
            style: AppTextStyles.actionSheetTextStyle,
          ),
        ),
        CupertinoActionSheetAction(
          isDefaultAction: true,
          onPressed: () {
            // sign up as a company
            AutoRouter.of(context).popAndPush(
              RegisterRoute(isCompanyRegister: true),
            );
          },
          child: Text(
            AppStrings.createCompanyAccountText,
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
