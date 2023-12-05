import 'dart:async';

import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/router.dart';

import 'splash_widgets.dart';
import 'package:flutter/material.dart';

@RoutePage()
class SplashScreen extends StatefulWidget {
  const SplashScreen({super.key});

  @override
  State<SplashScreen> createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen> {

  @override
  void initState() {
    super.initState();
    Timer(
      const Duration(seconds: 3),
          () => AutoRouter.of(context).popAndPush(const LoginRoute()),
    );
  }

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: CustomScrollView(
        slivers: [
          SliverToBoxAdapter(
            child: Column(
              children: [
                SizedBox(height: 262,),
                AppMainInfoWidget(),
                Padding(
                  padding: EdgeInsets.fromLTRB(66, 192, 66, 138),
                  child: LinearProgressIndicator(),
                ),
              ],
            ),
          ),
        ],
      )
    );
  }
}


