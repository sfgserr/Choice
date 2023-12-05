import 'package:choice/domain/blocs/login_bloc/login_bloc.dart';
import 'package:choice/domain/blocs/login_bloc/login_event.dart';
import 'package:choice/domain/blocs/login_bloc/login_state.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_keyboard_visibility/flutter_keyboard_visibility.dart';
import 'package:provider/provider.dart';
import 'login_widgets.dart';

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
    return BlocBuilder<LoginBloc, LoginState>(
      builder: (context, state) {
        return Scaffold(
          body: KeyboardVisibilityBuilder(
            builder: (context, isKeyboardVisible) {
              return Provider.value(
                value: isKeyboardVisible,
                child: CustomScrollView(
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
                                  onTap: () {},
                                  child: Text(
                                    AppStrings.createAccountText,
                                    textAlign: TextAlign.center,
                                    style:
                                        AppTextStyles.createAccountBtnTextStyle,
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
                                BlocProvider.of<LoginBloc>(context).add(
                                  ChangeTab(tabIndex: value),
                                );
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

                          BlocProvider.of<LoginBloc>(context)
                                      .state
                                      .currentTabIndex ==
                                  0
                              ? EmailView()
                              : PhoneView(),
                        ],
                      ),
                    ),
                  ],
                ),
              );
            },
          ),
        );
      },
    );
  }
}
