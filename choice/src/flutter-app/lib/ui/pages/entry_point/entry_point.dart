import 'dart:async';

import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/all_pages.dart';
import 'package:choice/config/router/router.dart';
import 'package:flutter/material.dart';

class EntryPointScreen extends StatefulWidget {
  const EntryPointScreen({super.key});

  @override
  State<EntryPointScreen> createState() => _EntryPointScreenState();
}

class _EntryPointScreenState extends State<EntryPointScreen> {


  @override
  Widget build(BuildContext context) {
    // auth logic
    return const SplashScreen();
  }
}
