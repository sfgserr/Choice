import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/all_pages.dart';
import 'package:choice/config/router/router.dart';
import 'package:choice/features/login/bloc/export_login_bloc.dart';
import 'package:choice/repositories/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/components/input_widget.dart';
import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/validators.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';


class GetCodeView extends StatefulWidget {
  const GetCodeView({super.key});

  @override
  State<GetCodeView> createState() => _GetCodeViewState();
}

class _GetCodeViewState extends State<GetCodeView> {
  late TextEditingController codeController;
  late FocusNode codeFocus;

  final _formKey = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
    codeController = TextEditingController();
    codeFocus = FocusNode();
  }

  @override
  void dispose() {
    codeController.dispose();
    codeFocus.dispose();
    super.dispose();
  }

  void loginTap() {
    if (_formKey.currentState!.validate()) {
      AutoRouter.of(context).popAndPush(const SplashRoute());
      FocusScope.of(context).unfocus();
      BlocProvider.of<LoginBloc>(context).add(
        GetCode(isGettingCode: false),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    bool isKeyboardVisible = Provider.of<bool>(context);
    return BlocBuilder<LoginBloc, LoginState>(
      builder: (context, state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // code
            Form(
              key: _formKey,
              child: InputWidget(
                inpwModel: InputWidgetModel(
                  autofocus: isKeyboardVisible,
                  validator: AppValidators.smsCodeValidator,
                  label: AppStrings.codeText,
                  hintText: AppStrings.inputCode,
                  onChangeTextField: (value) {
                    BlocProvider.of<LoginBloc>(context).add(EnableLoginBtn(
                      isLoginBtnEnabled: value.isNotEmpty,
                    ));
                  },
                  onFieldSubmitted: (value) => loginTap(),
                  controller: codeController,
                  focusNode: codeFocus,
                  keyboardType: TextInputType.number,
                ),
              ),
            ),

            MainButton(
              isEnabled: state.isLoginBtnEnabled,
              text: AppStrings.loginText,
              onTap: loginTap,
            ),
          ],
        );
      },
    );
  }
}
