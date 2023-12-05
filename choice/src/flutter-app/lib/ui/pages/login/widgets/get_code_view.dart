import 'package:choice/domain/blocs/login_bloc/export_login_bloc.dart';
import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/pages/login/models/input_widget_model.dart';
import 'package:choice/ui/pages/splash/splash_screen.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'input_widget.dart';

class GetCodeView extends StatefulWidget {
  const GetCodeView({super.key});

  @override
  State<GetCodeView> createState() => _GetCodeViewState();
}

class _GetCodeViewState extends State<GetCodeView> {
  late TextEditingController codeController;
  late FocusNode codeFocus;

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
    FocusScope.of(context).unfocus();
    // Navigator.push(
    //   context,
    //   MaterialPageRoute(
    //     builder: (context) => const SplashScreen(),
    //   ),
    // );
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<LoginBloc, LoginState>(
      builder: (context, state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // code
            InputWidget(
              inpwModel: InputWidgetModel(
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
                maxLength: 4, // sms code length
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
