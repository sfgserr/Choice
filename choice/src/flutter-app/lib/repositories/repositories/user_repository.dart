import 'dart:math';

import 'package:choice/main.dart';
import 'package:choice/repositories/storage/local_storage.dart';
import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

enum User { client, company }

class UserRepository {
  static String mainUrl = 'http://192.168.56.1:8080/api';

  String getEmailUrl = 'http://192.168.56.1:8080/api/Client/GetByEmail';
  String createNewClientUrl = 'http://192.168.56.1:8080/api/Client/Create';
  String createNewCompanyUrl = 'http://192.168.56.1:8080/api/Company/Create';

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

  void logInLocally() => LocalStorage.isLoggedIn = true;

  void logOutLocally() => LocalStorage.isLoggedIn = false;

  Future<Map<String, dynamic>?> getUserByEmail(String email) async {
    try {
      Response response = await _dio.get('$getEmailUrl?email=$email');
      return response.data;
    } catch (error) {
      logger.e("Error getting USER by EMAIL", error: error);
    }
  }

  Future<int?> createNewClient(
    String email,
    String password,
    String name,
    String surname,
  ) async {
    try {
      Response response = await _dio.post(
        createNewClientUrl,
        data: {
          "name": name,
          "surname": surname,
          "email": email,
          "password": password,
        },
      );
      return response.statusCode;
    } catch (e) {
      logger.e(e.toString());
    }
  }

  Future<int?> createNewCompany(
    String email,
    String password,
    String title,
  ) async {
    try {
      Response response = await _dio.post(
        createNewCompanyUrl,
        data: {
          "title": title,
          "email": email,
          "password": password,
        },
      );
      return response.statusCode;
    } catch (e) {
      logger.e(e.toString());
    }
  }
}
