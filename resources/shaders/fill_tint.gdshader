shader_type canvas_item;

uniform vec4 fill_color: hint_color = vec4(1.0);

uniform float amount: hint_range(0.0, 1.0, 0.1) = 0.0;

void fragment() {
    vec4 color = texture(TEXTURE, UV);
    vec3 rgb = mix(color.rgb, fill_color.rgb, round(amount));
    COLOR = vec4(rgb, color.a);
}
