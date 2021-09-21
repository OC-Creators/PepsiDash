Shader "Custom/Diffuse ZWrite" {
    Properties{
        _Color("Main Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
    }
        SubShader{
            Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
            LOD 200

        // デプスバッファのみにレンダリングする追加パス
        Pass {
            ZWrite On
            ColorMask 0
        }

        // Transparent/Diffuse からフォワードレンダリングのパスに貼り付けます 
        UsePass "Transparent/Diffuse/FORWARD"
    }
        Fallback "Transparent/VertexLit"
}