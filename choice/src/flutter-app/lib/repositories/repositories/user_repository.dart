import 'package:choice/repositories/storage/local_storage.dart';
import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

enum User { client, company }

class UserRepository {
  // static String mainUrl = 'http://194.154.66.72:8000/api';
  static String mainUrl = 'http://192.168.56.1:8080/api';
  var user = User.client;
  String getEmailUrl = 'http://192.168.56.1:8080/api/Client/GetByEmail';
  String signUpUrl = '$mainUrl/Client/Create';

  // final FlutterSecureStorage storage = const FlutterSecureStorage();
  final Dio _dio = Dio();

  // Future<bool> hasToken() async {
  //   var value = await storage.read(key: 'token');
  //   return value != null;
  // }

  // Future<void> persistToken(String token) async {
  //   await storage.write(key: 'token', value: token);
  // }

  // Future<void> deleteToken() async {
  //   storage.delete(key: 'token');
  //   storage.deleteAll();
  // }

  void logInLocally() {
    LocalStorage.isLoggedIn = true;
  }

  void logOutLocally() {
    LocalStorage.isLoggedIn = false;
  }

  Future<Map<String, dynamic>?> getUserByEmail(String email) async {
    try {
      Response response = await _dio
          .get('$getEmailUrl?email=$email');
      return response.data;
    } catch (error) {
      print(error.toString());
    }
  }
}
