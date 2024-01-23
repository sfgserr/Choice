import 'package:shared_preferences/shared_preferences.dart';

class LocalStorage {

  static late SharedPreferences prefs;

  static Future<void> initStorage() async {
    prefs = await SharedPreferences.getInstance();
  }

  static bool get isDarkMode => prefs.getBool('isDarkMode') ?? false;
  static set isDarkMode(bool value) => prefs.setBool('isDarkMode', value);

  static bool get isLoggedIn => prefs.getBool('isLoggedIn') ?? false;
  static set isLoggedIn(bool value) => prefs.setBool('isLoggedIn', value);

}