import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/router.dart';
import 'package:choice/features/auth/login/bloc/login_bloc.dart';
import '../../register/bloc/register_bloc.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class CreateAccountSheet extends StatelessWidget {
  const CreateAccountSheet({super.key});

  @override
  Widget build(BuildContext context) {
    return CupertinoActionSheet(
      actions: [
        CupertinoActionSheetAction(
          isDefaultAction: true,
          onPressed: () {
            // sign up as a client
            BlocProvider.of<LoginBloc>(context).add(ResetOptions());
            AutoRouter.of(context).popAndPush(const RegisterRoute());
            BlocProvider.of<RegisterBloc>(context).add(ClientRegister());
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
            BlocProvider.of<LoginBloc>(context).add(ResetOptions());
            AutoRouter.of(context).popAndPush(const RegisterRoute());
            BlocProvider.of<RegisterBloc>(context).add(CompanyRegister());
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
