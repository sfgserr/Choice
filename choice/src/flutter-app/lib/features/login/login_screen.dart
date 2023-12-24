import 'package:auto_route/auto_route.dart';
import 'package:choice/ui/components/app_main_info_widget.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_keyboard_visibility/flutter_keyboard_visibility.dart';
import 'package:provider/provider.dart';
import 'bloc/export_login_bloc.dart';
import 'login_widgets.dart';

@RoutePage()
class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen>
    with TickerProviderStateMixin {
  late TabController _tabController;

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 2, vsync: this);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: KeyboardVisibilityBuilder(
        builder: (context, isKeyboardVisible) {
          return Provider.value(
            value: isKeyboardVisible,
            child: BlocBuilder<LoginBloc, LoginState>(
              builder: (context, state) {
                return CustomScrollView(
                  slivers: [
                    SliverToBoxAdapter(
                      child: Column(
                        children: [
                          !isKeyboardVisible
                              ? const Column(
                                  crossAxisAlignment: CrossAxisAlignment.center,
                                  children: [
                                    SizedBox(
                                      height: 74,
                                    ),
                                    AppMainInfoWidget(),
                                    SizedBox(
                                      height: 61,
                                    ),
                                  ],
                                )
                              : const SizedBox(
                                  height: 55,
                                ),
                          Padding(
                            padding: const EdgeInsets.symmetric(horizontal: 16),
                            child: Row(
                              crossAxisAlignment: CrossAxisAlignment.end,
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              children: [
                                // authorization
                                Text(
                                  AppStrings.authHeader,
                                  style: AppTextStyles.authHeaderTextStyle,
                                ),

                                // create account
                                InkWell(
                                  splashColor: Colors.white,
                                  borderRadius: BorderRadius.circular(10),
                                  onTap: () {
                                    showCupertinoModalPopup(
                                      barrierColor: Colors.black45,
                                      context: context,
                                      builder: (_) {
                                        return const CreateAccountSheet();
                                      },
                                    );
                                  },
                                  child: Text(
                                    AppStrings.createAccountText,
                                    textAlign: TextAlign.center,
                                    style: AppTextStyles.textBtnTextStyle,
                                  ),
                                ),
                              ],
                            ),
                          ),

                          // tab bar
                          Padding(
                            padding: const EdgeInsets.symmetric(
                              horizontal: 16,
                              vertical: 8,
                            ),
                            child: TabBar(
                              controller: _tabController,
                              onTap: (value) {
                                if (value != state.currentTabIndex) {
                                  BlocProvider.of<LoginBloc>(context).add(
                                    EnableLoginBtn(isLoginBtnEnabled: false),
                                  );
                                  BlocProvider.of<LoginBloc>(context).add(
                                    ChangeTab(tabIndex: value),
                                  );
                                }
                              },
                              tabs: [
                                SizedBox(
                                  height: 48,
                                  child: Center(
                                    child: Text(AppStrings.emailText),
                                  ),
                                ),
                                SizedBox(
                                  height: 48,
                                  child: Center(
                                    child: Text(AppStrings.phoneText),
                                  ),
                                ),
                              ],
                            ),
                          ),

                          state.currentTabIndex == 0
                              ? const EmailView()
                              : state.isGettingCode
                                  ? const GetCodeView()
                                  : const PhoneView(),
                        ],
                      ),
                    ),
                  ],
                );
              },
            ),
          );
        },
      ),
    );
  }
}
