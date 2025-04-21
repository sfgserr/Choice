package com.app

import com.facebook.react.modules.network.OkHttpClientFactory
import com.facebook.react.modules.network.OkHttpClientProvider
import okhttp3.OkHttpClient
import java.security.cert.X509Certificate
import javax.net.ssl.*

class CustomClientFactory : OkHttpClientFactory {
    override fun createNewNetworkModuleClient(): OkHttpClient {
        val trustAllCerts = arrayOf<TrustManager>(object : X509TrustManager {
                override fun checkClientTrusted(chain: Array<out X509Certificate>?, authType: String?) {
                }

                override fun checkServerTrusted(chain: Array<out X509Certificate>?, authType: String?) {
                }

                override fun getAcceptedIssuers() = arrayOf<X509Certificate>()
            })
            // Install the all-trusting trust manager
            val sslContext = SSLContext.getInstance("SSL")
            sslContext.init(null, trustAllCerts, java.security.SecureRandom())
            // Create an ssl socket factory with our all-trusting manager
            val sslSocketFactory = sslContext.socketFactory

            return OkHttpClient.Builder()
                .sslSocketFactory(sslSocketFactory, trustAllCerts[0] as X509TrustManager)
                .cookieJar(com.facebook.react.modules.network.ReactCookieJarContainer())
                .hostnameVerifier { _, _ -> true }.build()
    }
}
